// import { Input } from '@angular/core';
// import { ArtService } from '../art.service';
// import { Art } from '../dto/Art';


// export class ArtChangeComponent {

// 	@Input()
// 	art: Art = new Art;

// 	resultMessage: string = '';

// 	constructor(private artService: ArtService) { }

// 	update(art: Art) {
// 		this.resultMessage = "";
// 		this.artService.update(art).subscribe(result => {
// 			console.log('Put scene result: ', result);
// 			this.resultMessage = "Completed successfully." + result;
// 		}, error => {
// 			console.log(`There was an issue. ${error._body}.`);
// 			this.resultMessage = `There was an issue. ${error._body}.`;
// 		});
// 	}
// }
