cmd��ʼ��Ǩ������,��IdentityServer��Ŀ�ļ�����ִ��cmd
dotnet ef migrations add InitialApplicationDbMigration -c ApplicationDbContext -p "../../Infrastructure/CoreDX.Application.DbMigration" -o Application
dotnet ef migrations add InitialApplicationDbMigration -c ApplicationIdentityDbContext -p "../../Infrastructure/CoreDX.Application.DbMigration" -o Identity
dotnet ef migrations add InitialApplicationDbMigration -c ApplicationPermissionDbContext -p "../../Infrastructure/CoreDX.Application.DbMigration" -o Permission

dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -p "../../Infrastructure/CoreDX.Application.DbMigration" -o IdentityServer/PersistedGrantDb
dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -p "../../Infrastructure/CoreDX.Application.DbMigration" -o IdentityServer/ConfigurationDb

dotnet ef migrations add InitialLocalizationDbMigration -c LocalizationModelContext -p "../../Infrastructure/CoreDX.Application.DbMigration" -o Application/LocalizationDb

vs2019��ʼ��Ǩ������ڰ��������̨
Add-Migration InitialApplicationDbMigration -Context ApplicationDbContext -Project CoreDX.Application.DbMigration -StartupProject IdentityServer -OutputDir Application
Add-Migration InitialApplicationIdentityDbMigration -Context ApplicationIdentityDbContext -Project CoreDX.Application.DbMigration -StartupProject IdentityServer -OutputDir Identity
Add-Migration InitialApplicationPermissionDbMigration -Context ApplicationPermissionDbContext -Project CoreDX.Application.DbMigration -StartupProject IdentityServer -OutputDir Permission

Add-Migration InitialIdentityServerPersistedGrantDbMigration -Context PersistedGrantDbContext -Project CoreDX.Application.DbMigration -StartupProject IdentityServer -OutputDir IdentityServer/PersistedGrantDb
Add-Migration InitialIdentityServerConfigurationDbMigration -Context ConfigurationDbContext -Project CoreDX.Application.DbMigration -StartupProject IdentityServer -OutputDir IdentityServer/ConfigurationDb

Add-Migration InitialLocalizationDbMigration -Context LocalizationModelContext -Project CoreDX.Application.DbMigration -StartupProject IdentityServer -OutputDir Application/LocalizationDb

            //�Զ�ɨ��Ǩ��ģ�Ͳ���������ʵ����ͼ
            migrationBuilder.CreateTreeEntityView(this,
                    AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName.Contains("Domain")))
				.CreateIdentityTreeEntityView(this, AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName.Contains("Domain")))
                //��ģ��ע��Ӧ�ñ����˵��
                .ApplyDatabaseDescription(this);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //ɾ������ʵ����ͼ�����û�취�Զ�ɨ��
            migrationBuilder.DropTreeEntityView("AppRoles")
                .DropTreeEntityView("TreeDomains")
				.DropTreeEntityView("Organizations")
				.DropTreeEntityView("Menus");

