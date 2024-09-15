import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DiscussionSettingsComponent } from './discussion-settings.component';

describe('DiscussionSettingsComponent', () => {
  let component: DiscussionSettingsComponent;
  let fixture: ComponentFixture<DiscussionSettingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DiscussionSettingsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DiscussionSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
