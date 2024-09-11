import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-portal-layout',
  templateUrl: './portal-layout.component.html',
  styleUrl: './portal-layout.component.scss'
})
export class PortalLayoutComponent implements OnInit {

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
      htmlTag.setAttribute('data-bs-theme', 'dark');
    } else {
      htmlTag.setAttribute('data-bs-theme', 'light');
    }
  }

  isDarkTheme() {
    return this.isDarkMode;
  }
}
