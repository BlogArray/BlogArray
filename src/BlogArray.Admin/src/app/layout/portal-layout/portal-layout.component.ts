import { Component, OnInit, ViewChild } from '@angular/core';
import { DropdownComponent } from '../../shared/ui/dropdown/dropdown.component';

@Component({
  selector: 'app-portal-layout',
  templateUrl: './portal-layout.component.html',
  styleUrl: './portal-layout.component.css'
})
export class PortalLayoutComponent implements OnInit {
  ngOnInit(): void {

  }
  mobileMenuOpen = false;

  toggleMobileMenu() {
    this.mobileMenuOpen = !this.mobileMenuOpen;
  }
}
