import { Artist, EmptyArtist } from "./Artist"

export class Art {
	artist: Artist = new EmptyArtist();
	title: string = '';
	rating: number = 0;
	entryDateTime!: Date;
	uri: string = '';
}
