import { Component, ViewContainerRef, Injector, OnInit, AfterViewInit } from '@angular/core';
import { AppConsts } from '@shared/AppConsts';
import { ActivatedRoute } from '@angular/router';
import { AppComponentBase } from '@shared/app-component-base';

import { SignalRHelper } from '@shared/helpers/SignalRHelper';
import { PluginObject } from '@shared/service-proxies/service-proxies';
import { PluginService } from '@app/plugins/plugin.service';
import { PluginComponent } from '@app/plugins/plugin.component';

@Component({
  templateUrl: './app.component.html'
})
export class AppComponent extends AppComponentBase implements OnInit, AfterViewInit {

  private viewContainerRef: ViewContainerRef;

  constructor(
    injector: Injector,
    private activatedRoute: ActivatedRoute,
    private pluginService: PluginService
  ) {
    super(injector);
  }

  ngOnInit(): void {
    if (this.appSession.application.features['SignalR']) {
      SignalRHelper.initSignalR();
    }

    abp.event.on('abp.notifications.received', userNotification => {
      abp.notifications.showUiNotifyForUserNotification(userNotification);

      //Desktop notification
      Push.create("AbpZeroTemplate", {
        body: userNotification.notification.data.message,
        icon: abp.appPath + 'assets/app-logo-small.png',
        timeout: 6000,
        onClick: function () {
          window.focus();
          this.close();
        }
      });
    });

    this.pluginService.loadPlugins().subscribe((plugins: PluginObject[]) => {
      for (let i = 0; i < plugins.length; i++) {
        const plugin = plugins[i];
        const pluginUrl = plugin.url.replace('/', '');
        const route = {path: this.pluginService.redirectUrlPrefix + pluginUrl, component: PluginComponent};
        this.activatedRoute.routeConfig.children.push(route);
      }
    });
  }

  ngAfterViewInit(): void {
    ($ as any).AdminBSB.activateAll();
    ($ as any).AdminBSB.activateDemo();
  }
}