package com.shindong.smartmanager.api.web.system;

import com.shindong.smartmanager.application.role.RoleRepository;
import com.shindong.smartmanager.application.role.RoleView;
import java.util.List;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/system/roles")
public class RoleController {

    private final RoleRepository roleRepository;

    public RoleController(RoleRepository roleRepository) {
        this.roleRepository = roleRepository;
    }

    @GetMapping
    public List<RoleResponse> list() {
        return roleRepository.findAllActive().stream()
                .map(RoleResponse::from)
                .toList();
    }

    public record RoleResponse(long id, String roleCode, String roleName) {
        static RoleResponse from(RoleView view) {
            return new RoleResponse(view.id(), view.roleCode(), view.roleName());
        }
    }
}
