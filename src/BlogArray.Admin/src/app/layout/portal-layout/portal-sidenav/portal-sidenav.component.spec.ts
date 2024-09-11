import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PortalSidenavComponent } from './portal-sidenav.component';

describe('PortalSidenavComponent', () => {
  let component: PortalSidenavComponent;
  let fixture: ComponentFixture<PortalSidenavComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PortalSidenavComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PortalSidenavComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
