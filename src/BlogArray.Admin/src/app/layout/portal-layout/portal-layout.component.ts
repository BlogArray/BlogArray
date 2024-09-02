import { Component, OnInit, ViewChild } from '@angular/core';
import { DropdownComponent } from '../../shared/ui/dropdown/dropdown.component';

@Component({
  selector: 'app-portal-layout',
  templateUrl: './portal-layout.component.html',
  styleUrl: './portal-layout.component.css'
})
export class PortalLayoutComponent implements OnInit {

  menuItems = [
    {
      name: 'Dashboard',
      link: 'dashboard',
      icon: 'chart-pie',
      items: []
    },
    {
      name: 'Posts',
      icon: 'pencil',
      items: [{
        name: 'All posts',
        link: 'posts',
      },
      {
        name: 'Add new post',
        link: 'post/new',
      }]
    },
    {
      name: 'Pages',
      icon: 'pencil',
      items: [{
        name: 'All pages',
        link: 'pages',
      },
      {
        name: 'Add new page',
        link: 'page/new',
      }]
    },
    {
      name: 'Categories',
      icon: 'squares-plus',
      items: [{
        name: 'All categories',
        link: 'categories',
      },
      {
        name: 'Add new category',
        link: 'categories/new',
      }]
    },
    {
      name: 'Media',
      icon: 'photo',
      items: [{
        name: 'Library',
        link: 'media',
      },
      {
        name: 'Add new media',
        link: 'media/new',
      }]
    },
    {
      name: 'Settings',
      icon: 'cog-6-tooth',
      items: [{
        name: 'General',
        link: 'pages',
      },
      {
        name: 'Email',
        link: 'page/new',
      },
      {
        name: 'Email',
        link: 'page/new',
      },
      {
        name: 'Email',
        link: 'page/new',
      },
      {
        name: 'Email',
        link: 'page/new',
      }]
    },
  ];

  ngOnInit(): void {

  }
  mobileMenuOpen = false;

  toggleMobileMenu() {
    this.mobileMenuOpen = !this.mobileMenuOpen;
  }
}
