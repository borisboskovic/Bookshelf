<template>
	<div class="search-wrapper">
		<div class="search-field-container">
			<SearchField
				@search-term-change="searchTermChangeHandler"
				:searchTerm="searchTerm"
				:placeholder="'Search...'"
			/>
		</div>
		<div v-if="books.length > 0 || authors.length > 0" class="search__body-container">
			<div class="search__body">
				<div class="search__body-section" v-if="books.length > 0">
					<div class="search__body-title">Books</div>
					<router-link
						class="search__result-item"
						:to="`/book-details/${book.bookIssueId}`"
						v-for="book in books"
						:key="book.bookIssueId"
						@click="itemSelectHandler"
					>
						<div class="search__image-container">
							<FallbackImage
								:defaultImage="defaultBookImage"
								:source="book.imageUrl"
							/>
						</div>
						<div class="search__result-item-text">
							{{ book.title }}
						</div>
					</router-link>
				</div>

				<div class="search__body-section" v-if="authors.length > 0">
					<div class="search__body-title">Authors</div>
					<router-link
						class="search__result-item"
						:to="`/author-details/${author.id}`"
						v-for="author in authors"
						:key="author.id"
						@click="itemSelectHandler"
					>
						<div class="search__image-container">
							<FallbackImage
								:defaultImage="defaultAuthorImage"
								:source="author.imageUrl"
							/>
						</div>
						<div class="search__result-item-text">
							{{ author.name }}
						</div>
					</router-link>
				</div>
			</div>
		</div>
	</div>
</template>

<script>
	import { ref } from "vue";
	import { debounce } from "lodash";
	import axios from "@/config/axios";
	import SearchField from "@/components/Ui/SearchField.vue";
	import FallbackImage from "@/components/Ui/Imaging/FallbackImage";
	import defaultBookImage from "@/assets/images/rasters/book-placeholder.jpg";
	import defaultAuthorImage from "@/assets/images/rasters/avatar-placeholder.png";

	export default {
		components: {
			SearchField,
			FallbackImage,
		},
		setup: () => {
			const searchTerm = ref("");
			const books = ref([]);
			const authors = ref([]);

			const searchTermChangeHandler = (value) => {
				searchTerm.value = value;
				searchHandler(value);
			};

			const searchHandler = debounce((value) => {
				if (value === "") {
					books.value = [];
					authors.value = [];
				} else {
					axios.get(`Home/Search?searchString=${value}`).then((response) => {
						books.value = response.data.books;
						authors.value = response.data.authors;
					});
				}
			}, 300);

			const itemSelectHandler = () => {
				(books.value = []), (authors.value = []);
				searchTerm.value = "";
			};

			return {
				searchTermChangeHandler,
				books,
				authors,
				defaultBookImage,
				defaultAuthorImage,
				itemSelectHandler,
				searchTerm,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./MainSearchComponent";
</style>
