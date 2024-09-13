import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'portal-header',
  templateUrl: './portal-header.component.html',
  styleUrl: './portal-header.component.scss'
})
export class PortalHeaderComponent {
  mobileMenuOpen = false;
  @Output() togleSidebar = new EventEmitter<boolean>();

  toggleMobileMenu() {
    this.mobileMenuOpen = !this.mobileMenuOpen;
    this.togleSidebar.emit(this.mobileMenuOpen);
  }
}
