package com.shindong.smartmanager.infrastructure.persistence.company;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.time.Instant;

@Entity
@Table(name = "company")
public class CompanyJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "company_name", nullable = false, length = 200)
    private String companyName;

    @Column(name = "president_name", nullable = false, length = 100)
    private String presidentName;

    @Column(name = "business_reg_no", nullable = false, length = 20)
    private String businessRegNo;

    @Column(name = "corporation_reg_no", length = 20)
    private String corporationRegNo;

    @Column(name = "business_address", nullable = false, length = 500)
    private String businessAddress;

    @Column(name = "homepage_url", length = 500)
    private String homepageUrl;

    @Column(name = "business_type", length = 100)
    private String businessType;

    @Column(name = "business_item", length = 100)
    private String businessItem;

    @Column(name = "telephone", length = 50)
    private String telephone;

    @Column(name = "fax", length = 50)
    private String fax;

    @Column(name = "sale_standard_day", columnDefinition = "TINYINT")
    private Integer saleStandardDay;

    @Column(name = "bill_approval_standard", columnDefinition = "TINYINT")
    private Integer billApprovalStandard;

    @Column(name = "fix_collect_day_1", columnDefinition = "TINYINT")
    private Integer fixCollectDay1;

    @Column(name = "contact_name", length = 100)
    private String contactName;

    @Column(name = "contact_email", length = 200)
    private String contactEmail;

    @Column(name = "recording_state", nullable = false, columnDefinition = "TINYINT")
    private int recordingState = 1;

    @Column(name = "created_by_id")
    private Long createdById;

    @Column(name = "created_at", nullable = false)
    private Instant createdAt;

    @Column(name = "updated_by_id")
    private Long updatedById;

    @Column(name = "updated_at")
    private Instant updatedAt;

    protected CompanyJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public String getCompanyName() {
        return companyName;
    }

    public void setCompanyName(String companyName) {
        this.companyName = companyName;
    }

    public String getPresidentName() {
        return presidentName;
    }

    public void setPresidentName(String presidentName) {
        this.presidentName = presidentName;
    }

    public String getBusinessRegNo() {
        return businessRegNo;
    }

    public void setBusinessRegNo(String businessRegNo) {
        this.businessRegNo = businessRegNo;
    }

    public String getCorporationRegNo() {
        return corporationRegNo;
    }

    public void setCorporationRegNo(String corporationRegNo) {
        this.corporationRegNo = corporationRegNo;
    }

    public String getBusinessAddress() {
        return businessAddress;
    }

    public void setBusinessAddress(String businessAddress) {
        this.businessAddress = businessAddress;
    }

    public String getHomepageUrl() {
        return homepageUrl;
    }

    public void setHomepageUrl(String homepageUrl) {
        this.homepageUrl = homepageUrl;
    }

    public String getBusinessType() {
        return businessType;
    }

    public void setBusinessType(String businessType) {
        this.businessType = businessType;
    }

    public String getBusinessItem() {
        return businessItem;
    }

    public void setBusinessItem(String businessItem) {
        this.businessItem = businessItem;
    }

    public String getTelephone() {
        return telephone;
    }

    public void setTelephone(String telephone) {
        this.telephone = telephone;
    }

    public String getFax() {
        return fax;
    }

    public void setFax(String fax) {
        this.fax = fax;
    }

    public Integer getSaleStandardDay() {
        return saleStandardDay;
    }

    public void setSaleStandardDay(Integer saleStandardDay) {
        this.saleStandardDay = saleStandardDay;
    }

    public Integer getBillApprovalStandard() {
        return billApprovalStandard;
    }

    public void setBillApprovalStandard(Integer billApprovalStandard) {
        this.billApprovalStandard = billApprovalStandard;
    }

    public Integer getFixCollectDay1() {
        return fixCollectDay1;
    }

    public void setFixCollectDay1(Integer fixCollectDay1) {
        this.fixCollectDay1 = fixCollectDay1;
    }

    public String getContactName() {
        return contactName;
    }

    public void setContactName(String contactName) {
        this.contactName = contactName;
    }

    public String getContactEmail() {
        return contactEmail;
    }

    public void setContactEmail(String contactEmail) {
        this.contactEmail = contactEmail;
    }

    public int getRecordingState() {
        return recordingState;
    }

    public void setRecordingState(int recordingState) {
        this.recordingState = recordingState;
    }

    public Long getCreatedById() {
        return createdById;
    }

    public void setCreatedById(Long createdById) {
        this.createdById = createdById;
    }

    public Instant getCreatedAt() {
        return createdAt;
    }

    public void setCreatedAt(Instant createdAt) {
        this.createdAt = createdAt;
    }

    public Long getUpdatedById() {
        return updatedById;
    }

    public void setUpdatedById(Long updatedById) {
        this.updatedById = updatedById;
    }

    public Instant getUpdatedAt() {
        return updatedAt;
    }

    public void setUpdatedAt(Instant updatedAt) {
        this.updatedAt = updatedAt;
    }
}
