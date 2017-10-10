import { Injectable, EventEmitter } from '@angular/core';
import { PluginServiceProxy, PluginObject } from '@shared/service-proxies/service-proxies';

@Injectable()
export class PluginService {
  pluginChange: EventEmitter<PluginObject[]> = new EventEmitter();
  redirectUrlPrefix = 'url_';

  constructor(pluginService: PluginServiceProxy) {
    pluginService.getPluginObjectsResult().subscribe((result: PluginObject[]) => {
        this.pluginChange.emit(result);
    });
  }

  loadPlugins() {
    return this.pluginChange;
  }
}
