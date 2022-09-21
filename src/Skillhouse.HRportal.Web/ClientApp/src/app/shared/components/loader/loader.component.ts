import { Component, Input, OnInit } from '@angular/core';
import { debounceTime } from 'rxjs/operators';
import { LoaderService } from './loader.service';

@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.scss']
})
export class LoaderComponent implements OnInit {
  isLoading = true;
  constructor(public loaderService: LoaderService) { }

  ngOnInit() {
    this.loaderService.loader$.pipe(debounceTime(10)).subscribe(d => {
      this.isLoading = d;
    }
    );
  }
}
