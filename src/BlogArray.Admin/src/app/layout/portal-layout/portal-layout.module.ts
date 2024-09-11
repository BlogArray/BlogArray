import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PortalLayoutComponent } from './portal-layout.component';
import { RouterModule } from '@angular/router';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { PortalHeaderComponent } from './portal-header/portal-header.component';
import { PortalSidenavComponent } from './portal-sidenav/portal-sidenav.component';



@NgModule({
  declarations: [PortalLayoutComponent, PortalHeaderComponent, PortalSidenavComponent],
  imports: [
    CommonModule,
    RouterModule,
    NgbDropdownModule
  ]
})
export class PortalLayoutModule { }
