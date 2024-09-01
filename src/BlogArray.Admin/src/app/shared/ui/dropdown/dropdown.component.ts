import { Component, ElementRef, HostListener, Input, TemplateRef } from '@angular/core';

@Component({
  selector: 'tw-dropdown',
  templateUrl: './dropdown.component.html',
  styleUrls: ['./dropdown.component.css']
})
export class DropdownComponent {
  @Input() triggerTemplate: TemplateRef<any> | null;
  isOpen: boolean = false;

  constructor(private elementRef: ElementRef) { }

  // Toggles the dropdown
  toggleDropdown() {
    this.isOpen = !this.isOpen;
  }

  // Close the dropdown when clicking outside of it
  @HostListener('document:click', ['$event'])
  clickOutside(event: Event) {
    if (!this.elementRef.nativeElement.contains(event.target)) {
      this.isOpen = false;
    }
  }
}
