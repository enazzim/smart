# Step 0 운영 DB export 보관

Tier B2·B3 산출물 및 export 스크립트.

| 파일 | 출처 | 상태 |
|------|------|------|
| [`puc-large-distinct.csv`](./puc-large-distinct.csv) | KIT_ERP 코드 감사 + 가설 | interim ✅ |
| [`ct-t-basis-menu.csv`](./ct-t-basis-menu.csv) | step0-plan D2 + Base.cs | draft ✅ |
| [`work-diary-group-seed.csv`](./work-diary-group-seed.csv) | UserInfo.aspx 00~16 그룹 | TO-BE 시드 |
| [`export-step0-data.ps1`](./export-step0-data.ps1) | DB 또는 interim 재생성 | |

## DB export (최종 검증)

```powershell
$env:SMARTMANAGER_ERP_DSN = "Server=...;Database=Sindong_ERP;User Id=...;Password=...;TrustServerCertificate=True"
.\export-step0-data.ps1
```

생성: `puc-large-distinct-from-db.csv`, `ct-t-basis-menu-from-db.csv` → 각각 `puc-large-distinct.csv`, `ct-t-basis-menu.csv`로 복사.

검증 SQL: [`step0-closure-checklist.md`](../step0-closure-checklist.md) §2 Tier B.

**주의:** `Web.config`의 DSN·비밀번호는 저장소에 커밋하지 마세요. 연결 문자열은 환경 변수만 사용합니다.
