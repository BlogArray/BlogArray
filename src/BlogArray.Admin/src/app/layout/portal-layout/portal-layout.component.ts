import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-portal-layout',
  templateUrl: './portal-layout.component.html',
  styleUrl: './portal-layout.component.scss'
})
export class PortalLayoutComponent implements OnInit {

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

  mobileMenuOpen = false;
  isDarkMode = false;

  ngOnInit(): void {

  }

  constructor(private router: Router) {
    // On initial load, check if the user has a theme preference in localStorage
    const theme = localStorage.getItem('theme');
    if (theme === 'dark') {
      this.isDarkMode = true;
      this.setDarkTheme(true);
    }
  }

  isSidemavOpened(path: any[]): boolean {
    return path.some(p => p.link == this.router.url);
  }

  toggleMobileMenu() {
    this.mobileMenuOpen = !this.mobileMenuOpen;
  }

  toggleTheme() {
    this.isDarkMode = !this.isDarkMode;
    this.setDarkTheme(this.isDarkMode);
    // Save the user's theme preference
    localStorage.setItem('theme', this.isDarkMode ? 'dark' : 'light');
  }

  setDarkTheme(isDark: boolean) {
    const htmlTag = document.documentElement;

    if (isDark) {
      htmlTag.classList.add('dark');
    } else {
      htmlTag.classList.remove('dark');
    }
  }

  isDarkTheme() {
    return this.isDarkMode;
  }
}
