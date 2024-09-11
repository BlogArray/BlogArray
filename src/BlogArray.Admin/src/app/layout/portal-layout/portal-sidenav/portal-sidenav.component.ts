import { Component } from '@angular/core';

@Component({
  selector: 'portal-sidenav',
  templateUrl: './portal-sidenav.component.html',
  styleUrl: './portal-sidenav.component.scss'
})
export class PortalSidenavComponent {

  menuItems = [
    {
      name: 'Dashboard',
      link: '/dashboard',
      icon: 'chart-pie',
      items: []
    },
    {
      name: 'Posts',
      icon: 'pencil',
      items: [{
        name: 'All posts',
        link: '/posts',
      },
      {
        name: 'Add new post',
        link: '/post/new',
      }]
    },
    {
      name: 'Pages',
      icon: 'document-duplicate',
      items: [{
        name: 'All pages',
        link: '/pages',
      },
      {
        name: 'Add new page',
        link: '/page/new',
      }]
    },
    {
      name: 'Categories',
      icon: 'squares-plus',
      items: [{
        name: 'All categories',
        link: '/categories',
      },
      {
        name: 'Add new category',
        link: '/categories/new',
      }]
    },
    {
      name: 'Comments',
      icon: 'chat-bubble-left-right',
      link: '/comments',
      items: []
    },
    {
      name: 'Media',
      icon: 'photo',
      items: [{
        name: 'Library',
        link: '/media',
      },
      {
        name: 'Add new media',
        link: '/media/new',
      }]
    },
    {
      name: 'Users',
      icon: 'users',
      items: [{
        name: 'All users',
        link: '/users',
      },
      {
        name: 'Add new user',
        link: '/users/new',
      }]
    },
    {
      name: 'Settings',
      icon: 'cog-6-tooth',
      items: [{
        name: 'General',
        link: '/settings/general',
      },
      {
        name: 'Email',
        link: '/settings/email',
      },
      {
        name: 'Menus',
        link: '/settings/menus',
      },
      {
        name: 'Pages',
        link: '/settings/pages',
      },
      {
        name: 'Media',
        link: '/settings/media',
      },
      {
        name: 'Comments',
        link: '/settings/comments',
      }
      ]
    },
  ];

}
