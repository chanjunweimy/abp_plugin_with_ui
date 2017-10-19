import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { EagerComponent } from './eager.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  headerItems: Object[] = [];

  constructor(private router: Router) {
    const routes = [
      {path: '', redirectTo: '/eager', pathMatch: 'full'},
      {path: 'eager', component: EagerComponent},
      {path: 'demoplugin', loadChildren: './demoplugin/demoplugin.module#DemoPluginModule'}
    ];

    this.headerItems.push(
      { classes: 'navLink', routerLink: 'eager', text: 'Eager' },
      { classes: 'navLink', routerLink: 'demoplugin', text: 'DemoPlugin' }
    );
    this.router.resetConfig(routes);
  }
}
