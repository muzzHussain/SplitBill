import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IndivisualGroupComponent } from './indivisual-group.module';

describe('IndivisualGroupComponent', () => {
  let component: IndivisualGroupComponent;
  let fixture: ComponentFixture<IndivisualGroupComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [IndivisualGroupComponent]
    });
    fixture = TestBed.createComponent(IndivisualGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
