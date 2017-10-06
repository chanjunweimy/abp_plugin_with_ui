import { Injectable } from '@angular/core';
import { ApiServiceProxy, PluginObject } from '../../shared/service-proxies/service-proxies';

@Injectable()
export class PluginService {
  plugins: PluginObject[] = [];

  constructor(apiService: ApiServiceProxy) {
      apiService.plugin().subscribe((result:PluginObject[])=>{
        this.plugins = result;
    });
  }

  loadPlugins() {
    return this.plugins;
  }
}
