import { Component, OnInit, Injector, Inject } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { PluginService } from '@app/plugins/plugin.service';
import { DOCUMENT, DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

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
        injector: Injector,
        public pluginService: PluginService,
        public sanitizer: DomSanitizer
    ) {
        super(injector);
        this.item.url = document.location.href.replace(this.pluginService.redirectUrlPrefix, '').replace('/app', '');
        //this.item.url = 'http://localhost:4201'
    }
    ngOnInit() {
        this.download = false; // hide iframe
        this.url = this.sanitizer.bypassSecurityTrustResourceUrl(this.item.url);
        this.download = true; // show iframe
    }
}
