import { Injectable, EventEmitter } from '@angular/core';
import { ApiServiceProxy, PluginObject } from '../../shared/service-proxies/service-proxies';

@Injectable()
export class PluginService {
  pluginChange: EventEmitter<PluginObject[]> = new EventEmitter();
  redirectUrlPrefix = 'url_';

  constructor(apiService: ApiServiceProxy) {
      apiService.plugin().subscribe((result: PluginObject[]) => {
        this.pluginChange.emit(result);
    });
  }

  loadPlugins() {
    return this.pluginChange;
  }
}
