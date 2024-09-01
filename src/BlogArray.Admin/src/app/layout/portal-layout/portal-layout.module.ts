import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PortalLayoutComponent } from './portal-layout.component';
import { ClarityModule } from '@clr/angular';
import { AppRoutingModule } from '../../app-routing.module';



@NgModule({
  declarations: [PortalLayoutComponent],
  imports: [
    CommonModule,
    AppRoutingModule,
    ClarityModule
  ]
})
export class PortalLayoutModule { }
