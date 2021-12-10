<template>
	<div class="search-wrapper">
		<div class="search-field-container">
			<SearchField @search-term-change="searchTermChangeHandler" :searchTerm="searchTerm" />
		</div>
		<div v-if="books.length > 0" class="search__body-container">
			<div class="search__body">
				<div class="search__body-section" v-if="books.length > 0">
					<div
						class="search__result-item"
						v-for="book in books"
						:key="book.id"
						@click="() => itemSelectHandler(book.id, book.title)"
					>
						<div class="search__result-item-text">
							{{ book.title }}
						</div>
					</div>
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

	export default {
		components: {
			SearchField,
		},
		emits: ["book-select"],
		setup: (_, context) => {
			const searchTerm = ref("");
			const books = ref([]);

			const searchTermChangeHandler = (value) => {
				searchTerm.value = value;
				searchHandler(value);
			};

			const searchHandler = debounce((value) => {
				if (value === "") {
					books.value = [];
				} else {
					axios.get(`BookDetails/SearchBooks?searchString=${value}`).then((response) => {
						books.value = response.data;
					});
				}
			}, 300);

			const itemSelectHandler = (id, title) => {
				books.value = [];
				searchTerm.value = "";
				context.emit("book-select", { id, title });
			};

			return {
				searchTermChangeHandler,
				books,
				itemSelectHandler,
				searchTerm,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./MainSearchComponent";
</style>
