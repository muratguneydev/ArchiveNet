import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ArtService } from '../art.service';
import { Art } from '../dto/Art';
import { Artist, EmptyArtist } from '../dto/Artist';
import { Name } from '../dto/Name';
import { EmptyNameCollection } from '../dto/NameCollection';

@Component({
  selector: 'app-artist-page',
  templateUrl: './artist-page.component.html',
  styleUrls: ['./artist-page.component.css']
})
export class ArtistPageComponent {

	artist: Artist = new EmptyArtist();
	artItems: Art[] = [];
	//imageUrl: string = '';// = `${this.artist.name.value}_${this.artist.id}}`;
	

	constructor(
		private route: ActivatedRoute,
		private artService: ArtService
	  ) { }
	
	  ngOnInit() {
		// First get the product id from the current route.
		const routeParams = this.route.snapshot.paramMap;
		const artistIdFromRoute = parseInt(routeParams.get('artistid')!);
		console.log('artistNameFromRoute: ', artistIdFromRoute);
		// this.artist = new Artist(new Name(artistIdFromRoute), new EmptyNameCollection());
		// Find the product that correspond with the id provided in route.
		//this.artItems = this.artService.getArtItems(artistNameFromRoute);

		this.artService.getArtItemsByArtist(artistIdFromRoute).subscribe(result => {
			console.log('Get art result: ', result);
			//console.log('TransferHttp [GET] /api/scenes/allresult', result);
			//let pagedResult = result as IScenePagedResult;
			this.artItems = result;
			this.artist = this.artItems[0].artist;
			//this.artist = new Artist(artistIdFromRoute, new Name(''), new EmptyNameCollection());
			//this.imageUrl = this.imageUrl;
			//this.totalCount = result.totalCount;
		});
	  }

	  
}
