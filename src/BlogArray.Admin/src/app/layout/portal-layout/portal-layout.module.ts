import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PortalLayoutComponent } from './portal-layout.component';
import { AppRoutingModule } from '../../app-routing.module';
import { DropdownModule } from '../../shared/ui/dropdown/dropdown.module';



@NgModule({
  declarations: [PortalLayoutComponent],
  imports: [
    CommonModule,
    AppRoutingModule,
    DropdownModule,
  ]
})
export class PortalLayoutModule { }
