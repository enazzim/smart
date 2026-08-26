package com.shindong.smartmanager.api.web.bom;

import com.shindong.smartmanager.api.security.BasisAuthorize;
import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.bom.ItemCompositionUpdateCommand;
import com.shindong.smartmanager.infrastructure.application.ItemCompositionApplicationService;
import jakarta.validation.Valid;
import java.util.List;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/basis/item-composition/plan")
@BasisAuthorize.ItemRead
public class ItemCompositionController {

    private final ItemCompositionApplicationService itemCompositionApplicationService;

    public ItemCompositionController(ItemCompositionApplicationService itemCompositionApplicationService) {
        this.itemCompositionApplicationService = itemCompositionApplicationService;
    }

    @GetMapping
    public List<ItemCompositionResponse> list(
            @RequestParam(required = false) Long parentItemId,
            @RequestParam(required = false) Long childItemId,
            @RequestParam(required = false) String parentItemNum,
            @RequestParam(required = false) String childItemNum
    ) {
        return itemCompositionApplicationService.listActive(
                parentItemId,
                childItemId,
                parentItemNum,
                childItemNum
        ).stream()
                .map(ItemCompositionResponse::from)
                .toList();
    }

    @PostMapping("/copy")
    @BasisAuthorize.ItemWrite
    public CopyBomResponse copy(
            @Valid @RequestBody CopyBomRequest request
    ) {
        String actor = SecurityUtils.requireLoginId();
        int copied = itemCompositionApplicationService.copyBom(
                request.sourceItemNum(),
                request.targetItemNum(),
                actor
        );
        return new CopyBomResponse(copied);
    }

    @GetMapping("/{itemNum}/explosion")
    public BomTreeNodeResponse explosion(@PathVariable String itemNum) {
        return BomTreeNodeResponse.from(itemCompositionApplicationService.explode(itemNum));
    }

    @GetMapping("/{itemNum}/reverse")
    public List<ItemCompositionResponse> reverse(@PathVariable String itemNum) {
        return itemCompositionApplicationService.reverse(itemNum).stream()
                .map(ItemCompositionResponse::from)
                .toList();
    }

    @PostMapping("/{itemNum}/lot-tracked/enable-preview")
    @BasisAuthorize.ItemWrite
    public List<LotTrackedEnablePreviewResponse> enableLotTrackedPreview(@PathVariable String itemNum) {
        return itemCompositionApplicationService.previewEnableLotTracked(itemNum).stream()
                .map(LotTrackedEnablePreviewResponse::from)
                .toList();
    }

    @PostMapping("/{itemNum}/lot-tracked/enable")
    @BasisAuthorize.ItemWrite
    public LotTrackedEnableResponse enableLotTracked(
            @PathVariable String itemNum,
            @Valid @RequestBody LotTrackedEnableRequest request
    ) {
        String actor = SecurityUtils.requireLoginId();
        int updated = itemCompositionApplicationService.enableLotTracked(
                itemNum,
                request.itemIds(),
                actor
        );
        return new LotTrackedEnableResponse(updated);
    }

    @GetMapping("/by-parent/{parentItemNum}")
    public List<ItemCompositionResponse> listByParent(@PathVariable String parentItemNum) {
        return itemCompositionApplicationService.listByParentItemNo(parentItemNum).stream()
                .map(ItemCompositionResponse::from)
                .toList();
    }

    @GetMapping("/id/{id}")
    public ItemCompositionResponse get(@PathVariable long id) {
        return ItemCompositionResponse.from(itemCompositionApplicationService.getActive(id));
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @BasisAuthorize.ItemWrite
    public ItemCompositionResponse create(
            @Valid @RequestBody CreateItemCompositionRequest request
    ) {
        String actor = SecurityUtils.requireLoginId();
        return ItemCompositionResponse.from(itemCompositionApplicationService.register(
                request.parentItemNum(),
                request.childItemNum(),
                request.parentQuantity(),
                request.childQuantity(),
                actor
        ));
    }

    @PutMapping("/{id}")
    @BasisAuthorize.ItemWrite
    public ItemCompositionResponse update(
            @PathVariable long id,
            @Valid @RequestBody UpdateItemCompositionRequest request
    ) {
        String actor = SecurityUtils.requireLoginId();
        return ItemCompositionResponse.from(itemCompositionApplicationService.update(
                id,
                new ItemCompositionUpdateCommand(request.parentQuantity(), request.childQuantity()),
                actor
        ));
    }

    @DeleteMapping("/{id}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @BasisAuthorize.ItemWrite
    public void delete(@PathVariable long id) {
        itemCompositionApplicationService.delete(id, SecurityUtils.requireLoginId());
    }
}
