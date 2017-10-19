import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { EagerComponent } from './eager.component';
import { BrowserModule } from '@angular/platform-browser';

@NgModule({
  declarations: [
    AppComponent,
    EagerComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot([])
  ],
  bootstrap: [AppComponent],
  entryComponents: [
    EagerComponent
  ]
})
export class AppModule { }
