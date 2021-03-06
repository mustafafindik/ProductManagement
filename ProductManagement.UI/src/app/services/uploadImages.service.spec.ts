/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { UploadImagesService } from './uploadImages.service';

describe('Service: UploadImages', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UploadImagesService]
    });
  });

  it('should ...', inject([UploadImagesService], (service: UploadImagesService) => {
    expect(service).toBeTruthy();
  }));
});
