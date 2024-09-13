import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PortalLayoutComponent } from './portal-layout.component';
import { RouterModule } from '@angular/router';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { PortalHeaderComponent } from './portal-header/portal-header.component';
import { PortalSidenavComponent } from './portal-sidenav/portal-sidenav.component';
import { TwCollapseModule } from '../../shared/ui/collapse/collapse.module';



@NgModule({
  declarations: [PortalLayoutComponent, PortalHeaderComponent, PortalSidenavComponent],
  imports: [
    CommonModule,
    RouterModule,
    NgbDropdownModule,
    TwCollapseModule
  ]
})
export class PortalLayoutModule { }
