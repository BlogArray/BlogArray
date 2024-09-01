import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PortalLayoutComponent } from './portal-layout.component';
import { AppRoutingModule } from '../../app-routing.module';
import { DropdownModule } from '../../shared/ui/dropdown/dropdown.module';
import { CollapseModule } from '../../shared/ui/collapse/collapse.module';



@NgModule({
  declarations: [PortalLayoutComponent],
  imports: [
    CommonModule,
    AppRoutingModule,
    DropdownModule,
    CollapseModule
  ]
})
export class PortalLayoutModule { }
