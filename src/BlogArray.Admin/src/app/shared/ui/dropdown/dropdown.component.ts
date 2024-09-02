import { Component, ElementRef, HostListener, Input } from '@angular/core';

@Component({
  selector: 'tw-dropdown',
  templateUrl: './dropdown.component.html',
  styleUrls: ['./dropdown.component.css']
})
export class DropdownComponent {
  @Input() isOpen: boolean = false;
  @Input() autoClose: boolean | "inside" | "outside" = true;

  constructor(private elementRef: ElementRef) { }

  // Toggles the dropdown
  toggleDropdown() {
    this.isOpen = !this.isOpen;
  }

  // Explicitly open the dropdown
  open() {
    this.isOpen = true;
  }

  // Explicitly close the dropdown
  close() {
    this.isOpen = false;
  }

  // Handle clicks outside the dropdown
  @HostListener('document:click', ['$event'])
  clickOutside(event: Event) {
    if (!this.elementRef.nativeElement.contains(event.target)) {
      if (this.autoClose === true || this.autoClose === 'outside') {
        this.close();
      }
    }
  }

  // Handle clicks inside the dropdown
  handleDropdownClick(event: Event) {
    if (this.autoClose === true || this.autoClose === 'inside') {
      this.close();
    }
    event.stopPropagation(); // Prevent clickOutside from triggering
  }
}
