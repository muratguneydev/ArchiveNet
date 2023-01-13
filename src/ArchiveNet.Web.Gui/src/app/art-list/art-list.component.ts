import { Component, Input } from '@angular/core';
import { Art } from '../dto/Art';

@Component({
  selector: 'app-art-list',
  templateUrl: './art-list.component.html',
  styleUrls: ['./art-list.component.css']
})
export class ArtListComponent {

	@Input()
	artItems: Art[] = [];

	selectedArt: Art = new Art;

	onEdit(art: Art) {
		this.selectedArt = art;
		console.log(art.uri);
	  }
}

//import { arts } from '../arts';

//arts = [...arts];

//Note: below is the content of arts.ts.
// export const arts: Art[] = [
// 	{
// 	  id: 1,
// 	  name: 'Phone XL',
// 	  price: 799,
// 	  description: 'A large phone with one of the best screens'
// 	},
// 	{
// 	  id: 2,
// 	  name: 'Phone Mini',
// 	  price: 699,
// 	  description: 'A great phone with one of the best cameras'
// 	},
// 	{
// 	  id: 3,
// 	  name: 'Phone Standard',
// 	  price: 299,
// 	  description: ''
// 	}
//   ];