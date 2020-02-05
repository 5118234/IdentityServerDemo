cmd ��ʼ��Ǩ������� IdentityServer ��Ŀ�ļ�����ִ�� cmd
```
dotnet ef migrations add InitialApplicationDbMigration -c ApplicationDbContext -p "../../Infrastructure/CoreDX.Application.DbMigration" -o Application
dotnet ef migrations add InitialApplicationDbMigration -c ApplicationIdentityDbContext -p "../../Infrastructure/CoreDX.Application.DbMigration" -o Identity
dotnet ef migrations add InitialApplicationDbMigration -c ApplicationPermissionDbContext -p "../../Infrastructure/CoreDX.Application.DbMigration" -o Permission

dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c IdentityServerConfigurationDbContext -p "../../Infrastructure/CoreDX.Application.DbMigration" -o IdentityServer/ConfigurationDb
dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c IdentityServerPersistedGrantDbContext -p "../../Infrastructure/CoreDX.Application.DbMigration" -o IdentityServer/PersistedGrantDb

dotnet ef migrations add InitialAdminAuditLogDbMigration -c AdminAuditLogDbContext -p "../../Infrastructure/CoreDX.Application.DbMigration" -o IdentityServer/AdminAuditLogDb
dotnet ef migrations add InitialAdminLogDbMigration -c AdminLogDbContext -p "../../Infrastructure/CoreDX.Application.DbMigration" -o IdentityServer/AdminLogDb

dotnet ef migrations add InitialLocalizationDbMigration -c LocalizationModelContext -p "../../Infrastructure/CoreDX.Application.DbMigration" -o Application/LocalizationDb
```

vs2019 ��ʼ��Ǩ������ڰ��������̨
```
Add-Migration InitialApplicationDbMigration -Context ApplicationDbContext -Project CoreDX.Application.DbMigration -StartupProject IdentityServer -OutputDir Application
Add-Migration InitialApplicationIdentityDbMigration -Context ApplicationIdentityDbContext -Project CoreDX.Application.DbMigration -StartupProject IdentityServer -OutputDir Identity
Add-Migration InitialApplicationPermissionDbMigration -Context ApplicationPermissionDbContext -Project CoreDX.Application.DbMigration -StartupProject IdentityServer -OutputDir Permission

Add-Migration InitialIdentityServerConfigurationDbMigration -Context IdentityServerConfigurationDbContext -Project CoreDX.Application.DbMigration -StartupProject IdentityServer -OutputDir IdentityServer/ConfigurationDb
Add-Migration InitialIdentityServerPersistedGrantDbMigration -Context IdentityServerPersistedGrantDbContext -Project CoreDX.Application.DbMigration -StartupProject IdentityServer -OutputDir IdentityServer/PersistedGrantDb

Add-Migration InitialAdminAuditLogDbMigration -Context AdminAuditLogDbContext -Project CoreDX.Application.DbMigration -StartupProject IdentityServer -OutputDir IdentityServer/AdminAuditLogDb
Add-Migration InitialAdminLogDbMigration -Context AdminLogDbContext -Project CoreDX.Application.DbMigration -StartupProject IdentityServer -OutputDir IdentityServer/AdminLogDb

Add-Migration InitialLocalizationDbMigration -Context LocalizationModelContext -Project CoreDX.Application.DbMigration -StartupProject IdentityServer -OutputDir Application/LocalizationDb
```

��Ǩ�����Զ�������ͼ������ MS SqlServer��
<br>�������� `protected override void OnModelCreating(ModelBuilder builder)`�е���`builder.ConfigDatabaseDescription();`
```
    //Up�Ľ�β��
    //�Զ�ɨ��Ǩ��ģ�Ͳ���������ʵ����ͼ
    migrationBuilder.ApplyDatabaseDescription(this);
    migrationBuilder.CreateTreeEntityView(TargetModel.GetEntityTypes());
}

protected override void Down(MigrationBuilder migrationBuilder)
{
    //Down�Ŀ�ʼ��
    //ɾ������ʵ����ͼ�����û�취�Զ�ɨ��
    migrationBuilder.DropTreeEntityView("AppRoles")
        .DropTreeEntityView("TreeDomains")
    .DropTreeEntityView("Organizations")
    .DropTreeEntityView("Menus");

    Add-Migration InitialTestDbMigration -Context TestDbContext -Project CoreDX.Application.DbMigration -StartupProject IdentityServer -OutputDir Test
```