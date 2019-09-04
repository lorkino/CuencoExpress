import { TestBed } from '@angular/core/testing';

import { PushsuscriberService } from './pushsuscriber.service';

describe('PushsuscriberService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PushsuscriberService = TestBed.get(PushsuscriberService);
    expect(service).toBeTruthy();
  });
});
