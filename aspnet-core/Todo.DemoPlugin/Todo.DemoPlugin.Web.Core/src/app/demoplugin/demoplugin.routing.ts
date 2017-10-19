import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DemoPluginComponent } from './demoplugin.component';

const routes: Routes = [
  { path: '', component: DemoPluginComponent }
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
