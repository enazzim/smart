package com.shindong.smartmanager.api.web.item;

import com.shindong.smartmanager.application.item.ItemCommand;
import com.shindong.smartmanager.application.item.ItemUpdateCommand;
import com.shindong.smartmanager.infrastructure.application.ItemApplicationService;
import jakarta.validation.Valid;
import java.util.List;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/basis/items")
public class ItemController {

    private static final String DEFAULT_ACTOR = "local-dev";

    private final ItemApplicationService itemApplicationService;

    public ItemController(ItemApplicationService itemApplicationService) {
        this.itemApplicationService = itemApplicationService;
    }

    @GetMapping
    public List<ItemResponse> list(
            @RequestParam(required = false) String itemNo,
            @RequestParam(required = false) String itemName
    ) {
        return itemApplicationService.listActive(itemNo, itemName).stream()
                .map(ItemResponse::from)
                .toList();
    }

    @GetMapping("/{id}")
    public ItemResponse get(@PathVariable long id) {
        return ItemResponse.from(itemApplicationService.getActive(id));
    }

    @GetMapping("/by-no/{itemNo}")
    public ItemResponse getByItemNo(@PathVariable String itemNo) {
        return ItemResponse.from(itemApplicationService.getActiveByItemNo(itemNo));
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    public ItemResponse create(
            @Valid @RequestBody CreateItemRequest request,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String actor = actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
        ItemCommand command = new ItemCommand(
                request.itemNo(),
                request.itemName(),
                request.propertyClassification(),
                request.unit(),
                request.standard(),
                request.standardUnitCost(),
                request.checkDistinction(),
                request.leadTime(),
                request.safetyStockQuantity(),
                request.orderIntervalQuantity(),
                request.minOrderQuantity()
        );
        return ItemResponse.from(itemApplicationService.register(command, actor));
    }

    @PutMapping("/{id}")
    public ItemResponse update(
            @PathVariable long id,
            @Valid @RequestBody UpdateItemRequest request,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String actor = actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
        ItemUpdateCommand command = new ItemUpdateCommand(
                request.itemName(),
                request.propertyClassification(),
                request.unit(),
                request.standard(),
                request.standardUnitCost(),
                request.checkDistinction(),
                request.leadTime(),
                request.safetyStockQuantity(),
                request.orderIntervalQuantity(),
                request.minOrderQuantity()
        );
        return ItemResponse.from(itemApplicationService.update(id, command, actor));
    }

    @DeleteMapping("/{id}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    public void delete(
            @PathVariable long id,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String actor = actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
        itemApplicationService.delete(id, actor);
    }
}
