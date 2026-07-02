package com.shindong.smartmanager.infrastructure.persistence.inventory;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.time.Instant;

@Entity
@Table(name = "inventory_location")
public class InventoryLocationJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "location_code", nullable = false, length = 20)
    private String locationCode;

    @Column(name = "location_name", nullable = false, length = 100)
    private String locationName;

    protected InventoryLocationJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public String getLocationCode() {
        return locationCode;
    }
}
