import { Component, Input } from '@angular/core';
import { ArtService } from '../art.service';
import { Art } from '../dto/Art';
import { Name } from '../dto/Name';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent {

	artItems: Art[] = [];
	dateOffset: number = 2069;

	constructor(
		private artService: ArtService
	  ) { }
	
	  ngOnInit() {
		this.artService.getArtItemsByEntryDate(this.dateOffset).subscribe(result => {
			console.log('Get art result: ', result);
			this.artItems = result;
		});
	  }

	  refresh(dateOffset: number) {
		this.artService.getArtItemsByEntryDate(dateOffset).subscribe(result => {
			console.log('Get art result: ', result);
			this.artItems = result;
		});
	}
}
