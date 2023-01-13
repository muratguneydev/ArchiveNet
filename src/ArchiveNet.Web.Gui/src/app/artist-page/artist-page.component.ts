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

	constructor(
		private route: ActivatedRoute,
		private artService: ArtService
	  ) { }
	
	  ngOnInit() {
		// First get the product id from the current route.
		const routeParams = this.route.snapshot.paramMap;
		const artistNameFromRoute = routeParams.get('artistname')!;
		console.log('artistNameFromRoute: ', artistNameFromRoute);
		this.artist = new Artist(new Name(artistNameFromRoute), new EmptyNameCollection());
		// Find the product that correspond with the id provided in route.
		//this.artItems = this.artService.getArtItems(artistNameFromRoute);

		this.artService.getArtItemsByArtist(artistNameFromRoute).subscribe(result => {
			console.log('Get art result: ', result);
			//console.log('TransferHttp [GET] /api/scenes/allresult', result);
			//let pagedResult = result as IScenePagedResult;
			this.artItems = result;
			//this.totalCount = result.totalCount;
		});
	  }
}
