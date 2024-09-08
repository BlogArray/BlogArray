import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PortalLayoutComponent } from './portal-layout.component';
import { TwDropdownModule } from '../../shared/ui/dropdown/dropdown.module';
import { TwCollapseModule } from '../../shared/ui/collapse/collapse.module';
import { IconsModule } from '../../shared/ui/icons/icons.module';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [PortalLayoutComponent],
  imports: [
    CommonModule,
    RouterModule,
    TwDropdownModule,
    TwCollapseModule,
    IconsModule
  ]
})
export class PortalLayoutModule { }
