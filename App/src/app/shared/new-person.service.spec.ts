import { TestBed } from '@angular/core/testing';

import { NewPersonService } from './new-person.service';

describe('NewPersonService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: NewPersonService = TestBed.get(NewPersonService);
    expect(service).toBeTruthy();
  });
});
