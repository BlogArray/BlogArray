import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PortalHeaderComponent } from './portal-header.component';

describe('PortalHeaderComponent', () => {
  let component: PortalHeaderComponent;
  let fixture: ComponentFixture<PortalHeaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PortalHeaderComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PortalHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
