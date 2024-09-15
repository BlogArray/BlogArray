import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'portal-header',
  templateUrl: './portal-header.component.html',
  styleUrl: './portal-header.component.scss'
})
export class PortalHeaderComponent {
  mobileMenuOpen = false;
  @Output() togleSidebar = new EventEmitter<boolean>();

  menuItems = [
    {
      name: 'Posts',
      icon: 'pencil',
      link: '/post/edit'
    },
    {
      name: 'Pages',
      icon: 'files',
      link: '/page/edit'
    },
    {
      name: 'Categories',
      icon: 'grid',
      link: '/categories/edit'
    },
    {
      name: 'Media',
      icon: 'image',
      link: '/media/upload'
    },
    {
      name: 'Users',
      icon: 'people',
      link: '/users/add'
    },
  ];

  toggleMobileMenu() {
    this.mobileMenuOpen = !this.mobileMenuOpen;
    this.togleSidebar.emit(this.mobileMenuOpen);
  }
}
