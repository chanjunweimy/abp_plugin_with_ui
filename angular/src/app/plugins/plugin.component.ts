import { Compiler, Component, OnInit, Injector, Inject, NgModule } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { PluginService } from '@app/plugins/plugin.service';
import { DOCUMENT, DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

declare var SystemJS;

@Component({
    selector: 'app-plugin-component',
    templateUrl: './plugin.component.html'
})
export class PluginComponent extends AppComponentBase implements OnInit {
    item = {url : ''}
    url: SafeResourceUrl;

    // toggle iframe
    download = false;
    constructor(
        @Inject(DOCUMENT) private document,
        private _compiler: Compiler,
        private _injector: Injector,
        public pluginService: PluginService,
        public sanitizer: DomSanitizer
    ) {
        super(_injector);
        //this.item.url = document.location.href.replace(this.pluginService.redirectUrlPrefix, '').replace('/app', '');
        //this.item.url = 'http://localhost:4201'
    }
    ngOnInit() {
        //this.download = false; // hide iframe
        //this.url = this.sanitizer.bypassSecurityTrustResourceUrl(this.item.url);
        //this.download = true; // show iframe

        SystemJS.load('plugins/demoplugin/demoplugin.module').then(module => {
            const mod = module;
            console.log(mod);
            /*
            this._compiler.compileModuleAndAllComponentsAsync(module)
            .then((factories) => {
                const moduleRef = factories.ngModuleFactory.create(this._injector);
                console.log(moduleRef);
            });
            */
        });
    }
}
