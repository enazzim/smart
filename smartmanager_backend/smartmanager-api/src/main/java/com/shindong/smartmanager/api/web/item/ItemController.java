package com.shindong.smartmanager.api.web.item;

import com.shindong.smartmanager.api.security.BasisAuthorize;
import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.item.ItemCommand;
import com.shindong.smartmanager.application.item.ItemUpdateCommand;
import com.shindong.smartmanager.infrastructure.application.ItemApplicationService;
import jakarta.validation.Valid;
import java.util.List;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PatchMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/basis/items")
@BasisAuthorize.ItemRead
public class ItemController {

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
    @BasisAuthorize.ItemWrite
    public ItemResponse create(
            @Valid @RequestBody CreateItemRequest request
    ) {
        String actor = SecurityUtils.requireLoginId();
        ItemCommand command = new ItemCommand(
                request.itemNo(),
                request.itemName(),
                request.propertyClassification(),
                request.modelType(),
                request.unit(),
                request.standard(),
                request.standardUnitCost(),
                request.checkDistinction(),
                request.leadTime(),
                request.safetyStockQuantity(),
                request.orderIntervalQuantity(),
                request.minOrderQuantity(),
                request.lotTrackedOrDefault()
        );
        return ItemResponse.from(itemApplicationService.register(command, actor));
    }

    @PutMapping("/{id}")
    @BasisAuthorize.ItemWrite
    public ItemResponse update(
            @PathVariable long id,
            @Valid @RequestBody UpdateItemRequest request
    ) {
        String actor = SecurityUtils.requireLoginId();
        ItemUpdateCommand command = new ItemUpdateCommand(
                request.itemName(),
                request.propertyClassification(),
                request.modelType(),
                request.unit(),
                request.standard(),
                request.standardUnitCost(),
                request.checkDistinction(),
                request.leadTime(),
                request.safetyStockQuantity(),
                request.orderIntervalQuantity(),
                request.minOrderQuantity(),
                request.lotTrackedOrDefault()
        );
        return ItemResponse.from(itemApplicationService.update(id, command, actor));
    }

    @PatchMapping("/{id}/lot-tracked")
    @BasisAuthorize.ItemWrite
    public ItemResponse updateLotTracked(
            @PathVariable long id,
            @Valid @RequestBody UpdateLotTrackedRequest request
    ) {
        String actor = SecurityUtils.requireLoginId();
        return ItemResponse.from(itemApplicationService.updateLotTracked(
                id,
                Boolean.TRUE.equals(request.lotTracked()),
                actor
        ));
    }

    @DeleteMapping("/{id}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @BasisAuthorize.ItemWrite
    public void delete(@PathVariable long id) {
        itemApplicationService.delete(id, SecurityUtils.requireLoginId());
    }
}
