import { Component, OnInit } from '@angular/core';

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

  constructor() {
    // On initial load, check if the user has a theme preference in localStorage
    const theme = localStorage.getItem('theme');
    if (theme === 'dark') {
      this.isDarkMode = true;
      this.setDarkTheme(true);
    }
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
