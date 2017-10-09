import { Component, Injector, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { MenuItem } from '@shared/layout/menu-item';
import { PluginService } from 'app/plugins/plugin.service';
import { PluginObject } from '../../shared/service-proxies/service-proxies';

@Component({
    templateUrl: './sidebar-nav.component.html',
    selector: 'sidebar-nav',
    encapsulation: ViewEncapsulation.None
})
export class SideBarNavComponent extends AppComponentBase {
    menuItems: MenuItem[] = [
        new MenuItem(this.l("HomePage"), "", "home", "/app/home"),

        new MenuItem(this.l("Tenants"), "Pages.Tenants", "business", "/app/tenants"),
        new MenuItem(this.l("Users"), "Pages.Users", "people", "/app/users"),
        new MenuItem(this.l("Roles"), "Pages.Roles", "local_offer", "/app/roles"),
        new MenuItem(this.l("About"), "", "info", "/app/about"),

        new MenuItem(this.l("MultiLevelMenu"), "", "menu", "", [
            new MenuItem("ASP.NET Boilerplate", "", "", "", [
                new MenuItem("Home", "", "", "https://aspnetboilerplate.com/?ref=abptmpl"),
                new MenuItem("Templates", "", "", "https://aspnetboilerplate.com/Templates?ref=abptmpl"),
                new MenuItem("Samples", "", "", "https://aspnetboilerplate.com/Samples?ref=abptmpl"),
                new MenuItem("Documents", "", "", "https://aspnetboilerplate.com/Pages/Documents?ref=abptmpl")
            ]),
            new MenuItem("ASP.NET Zero", "", "", "", [
                new MenuItem("Home", "", "", "https://aspnetzero.com?ref=abptmpl"),
                new MenuItem("Description", "", "", "https://aspnetzero.com/?ref=abptmpl#description"),
                new MenuItem("Features", "", "", "https://aspnetzero.com/?ref=abptmpl#features"),
                new MenuItem("Pricing", "", "", "https://aspnetzero.com/?ref=abptmpl#pricing"),
                new MenuItem("Faq", "", "", "https://aspnetzero.com/Faq?ref=abptmpl"),
                new MenuItem("Documents", "", "", "https://aspnetzero.com/Documents?ref=abptmpl")
            ])
        ])
    ];

    constructor(
        injector: Injector,
        pluginService: PluginService
    ) {
        super(injector);
        pluginService.loadPlugins().subscribe((plugins: PluginObject[]) => {
            const menus: MenuItem[] = [];
            for (let i = 0; i < plugins.length; i++) {
                const plugin = plugins[i];
                const pluginUrl = plugin.url.replace('/', '');
                const menu = new MenuItem(plugin.title, '', '', pluginService.redirectUrlPrefix + pluginUrl);
                this.menuItems.push(menu);
            }
        });
    }

    showMenuItem(menuItem): boolean {
        if (menuItem.permissionName) {
            return this.permission.isGranted(menuItem.permissionName);
        }

        return true;
    }
}