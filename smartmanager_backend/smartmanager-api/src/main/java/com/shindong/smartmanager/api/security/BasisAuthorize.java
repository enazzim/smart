package com.shindong.smartmanager.api.security;

import java.lang.annotation.ElementType;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.lang.annotation.Target;
import org.springframework.security.access.prepost.PreAuthorize;

public final class BasisAuthorize {

    private BasisAuthorize() {
    }

    @Target(ElementType.TYPE)
    @Retention(RetentionPolicy.RUNTIME)
    @PreAuthorize("hasAuthority('basis:company:read')")
    public @interface CompanyRead {
    }

    @Target({ElementType.METHOD, ElementType.TYPE})
    @Retention(RetentionPolicy.RUNTIME)
    @PreAuthorize("hasAuthority('basis:company:write')")
    public @interface CompanyWrite {
    }

    @Target(ElementType.TYPE)
    @Retention(RetentionPolicy.RUNTIME)
    @PreAuthorize("hasAuthority('basis:item:read')")
    public @interface ItemRead {
    }

    @Target({ElementType.METHOD, ElementType.TYPE})
    @Retention(RetentionPolicy.RUNTIME)
    @PreAuthorize("hasAuthority('basis:item:write')")
    public @interface ItemWrite {
    }

    @Target(ElementType.TYPE)
    @Retention(RetentionPolicy.RUNTIME)
    @PreAuthorize("hasAuthority('basis:process:read')")
    public @interface ProcessRead {
    }

    @Target({ElementType.METHOD, ElementType.TYPE})
    @Retention(RetentionPolicy.RUNTIME)
    @PreAuthorize("hasAuthority('basis:process:write')")
    public @interface ProcessWrite {
    }

    @Target(ElementType.TYPE)
    @Retention(RetentionPolicy.RUNTIME)
    @PreAuthorize("hasAuthority('basis:unit-price:read')")
    public @interface UnitPriceRead {
    }

    @Target({ElementType.METHOD, ElementType.TYPE})
    @Retention(RetentionPolicy.RUNTIME)
    @PreAuthorize("hasAuthority('basis:unit-price:write')")
    public @interface UnitPriceWrite {
    }

    @Target(ElementType.TYPE)
    @Retention(RetentionPolicy.RUNTIME)
    @PreAuthorize("hasAuthority('basis:work-center:read')")
    public @interface WorkCenterRead {
    }

    @Target({ElementType.METHOD, ElementType.TYPE})
    @Retention(RetentionPolicy.RUNTIME)
    @PreAuthorize("hasAuthority('basis:work-center:write')")
    public @interface WorkCenterWrite {
    }

    @Target(ElementType.TYPE)
    @Retention(RetentionPolicy.RUNTIME)
    @PreAuthorize("hasAuthority('basis:work-standard:read')")
    public @interface WorkStandardRead {
    }

    @Target({ElementType.METHOD, ElementType.TYPE})
    @Retention(RetentionPolicy.RUNTIME)
    @PreAuthorize("hasAuthority('basis:work-standard:write')")
    public @interface WorkStandardWrite {
    }

    @Target(ElementType.TYPE)
    @Retention(RetentionPolicy.RUNTIME)
    @PreAuthorize("hasAuthority('basis:equipment:read')")
    public @interface EquipmentRead {
    }

    @Target({ElementType.METHOD, ElementType.TYPE})
    @Retention(RetentionPolicy.RUNTIME)
    @PreAuthorize("hasAuthority('basis:equipment:write')")
    public @interface EquipmentWrite {
    }

    @Target(ElementType.TYPE)
    @Retention(RetentionPolicy.RUNTIME)
    @PreAuthorize("hasAuthority('basis:user:read')")
    public @interface UserRead {
    }

    @Target({ElementType.METHOD, ElementType.TYPE})
    @Retention(RetentionPolicy.RUNTIME)
    @PreAuthorize("hasAuthority('basis:user:write')")
    public @interface UserWrite {
    }

    @Target(ElementType.TYPE)
    @Retention(RetentionPolicy.RUNTIME)
    @PreAuthorize("hasAuthority('basis:production-calendar:read')")
    public @interface ProductionCalendarRead {
    }

    @Target({ElementType.METHOD, ElementType.TYPE})
    @Retention(RetentionPolicy.RUNTIME)
    @PreAuthorize("hasAuthority('basis:production-calendar:write')")
    public @interface ProductionCalendarWrite {
    }

    @Target(ElementType.TYPE)
    @Retention(RetentionPolicy.RUNTIME)
    @PreAuthorize("hasAuthority('basis:public-code:read')")
    public @interface PublicCodeRead {
    }

    @Target(ElementType.TYPE)
    @Retention(RetentionPolicy.RUNTIME)
    @PreAuthorize("hasAuthority('system:role:read')")
    public @interface RoleRead {
    }
}
