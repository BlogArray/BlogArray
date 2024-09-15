import { Component } from '@angular/core';
import { Router } from '@angular/router';

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
      icon: 'speedometer2',
      items: []
    },
    {
      name: 'Posts',
      icon: 'pencil',
      items: [{
        name: 'All posts',
        link: '/posts/list',
      },
      {
        name: 'Add new post',
        link: '/post/edit',
      }]
    },
    {
      name: 'Pages',
      icon: 'files',
      items: [{
        name: 'All pages',
        link: '/pages/list',
      },
      {
        name: 'Add new page',
        link: '/page/edit',
      }]
    },
    {
      name: 'Categories',
      icon: 'grid',
      items: [{
        name: 'All categories',
        link: '/categories/list',
      },
      {
        name: 'Add new category',
        link: '/categories/edit',
      }]
    },
    {
      name: 'Comments',
      icon: 'chat-left-text',
      link: '/comments',
      items: []
    },
    {
      name: 'Media',
      icon: 'image',
      items: [{
        name: 'Library',
        link: '/media/library',
      },
      {
        name: 'Add new media',
        link: '/media/upload',
      }]
    },
    {
      name: 'Users',
      icon: 'people',
      items: [{
        name: 'All users',
        link: '/users/list',
      },
      {
        name: 'Add new user',
        link: '/users/add',
      }]
    },
    {
      name: 'Settings',
      icon: 'gear',
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

  constructor(private router: Router) {

  }

  isSidemavOpened(path: any[]): boolean {
    return path.some(p => p.link == this.router.url);
  }
}
