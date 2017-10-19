import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { DemoPluginComponent } from './demoplugin.component';
import { NumberOnlyDirective } from './numberonly.directive';
import { routing } from './demoplugin.routing';

@NgModule({
  declarations: [
    DemoPluginComponent,
    NumberOnlyDirective
  ],
  imports: [
    HttpClientModule,
    routing
  ],
  providers: []
})
export class DemoPluginModule { }
