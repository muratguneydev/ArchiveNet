import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ArtService } from '../art.service';
import { Art } from '../dto/Art';
import { Artist, EmptyArtist } from '../dto/Artist';
import { Name } from '../dto/Name';
import { EmptyNameCollection } from '../dto/NameCollection';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent {

	artItems: Art[] = [];

	constructor(
		private artService: ArtService
	  ) { }
	
	  ngOnInit() {
		this.artService.getArtItemsByEntryDate(2065).subscribe(result => {
			console.log('Get art result: ', result);
			this.artItems = result;
		});
	  }
}
