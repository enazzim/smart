package com.shindong.smartmanager.api.web.code;

import com.shindong.smartmanager.api.security.BasisAuthorize;
import com.shindong.smartmanager.application.code.CodeGroupOptionsRepository;
import com.shindong.smartmanager.application.code.CodeOptionView;
import java.util.List;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/basis/code-groups")
@BasisAuthorize.PublicCodeRead
public class CodeGroupController {

    private final CodeGroupOptionsRepository codeGroupOptionsRepository;

    public CodeGroupController(CodeGroupOptionsRepository codeGroupOptionsRepository) {
        this.codeGroupOptionsRepository = codeGroupOptionsRepository;
    }

    @GetMapping("/{codeGroupKey}/options")
    public List<CodeOptionResponse> options(@PathVariable String codeGroupKey) {
        return codeGroupOptionsRepository.findActiveOptions(codeGroupKey).stream()
                .map(CodeOptionResponse::from)
                .toList();
    }

    public record CodeOptionResponse(long id, String code, String name) {
        static CodeOptionResponse from(CodeOptionView view) {
            return new CodeOptionResponse(view.id(), view.code(), view.name());
        }
    }
}
