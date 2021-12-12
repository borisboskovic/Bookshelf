<template>
	<div class="page-container">
		<div class="side-container">
			<div class="book-list-names">
				<h2>Bookshelves</h2>
				<div :class="`book-list-names__item ${selectedName === 'all' ? 'active' : ''}`">
					<div class="book-list-names__name" @click="(event) => setSelectedList('all')">
						All
					</div>
					<div class="book-list-count">
						({{ currentlyReadingCount + readCount + wantToReadCount }})
					</div>
				</div>
				<div
					:class="`book-list-names__item ${
						selectedName === 'currentlyReading' ? 'active' : ''
					}`"
				>
					<div
						class="book-list-names__name"
						@click="() => setSelectedList('currentlyReading')"
					>
						Currently Reading
					</div>
					<div class="book-list-count">({{ currentlyReadingCount }})</div>
				</div>
				<div :class="`book-list-names__item ${selectedName === 'read' ? 'active' : ''}`">
					<div class="book-list-names__name" @click="(event) => setSelectedList('read')">
						Finished Books
					</div>
					<div class="book-list-count">({{ readCount }})</div>
				</div>
				<div
					:class="`book-list-names__item ${
						selectedName === 'wantToRead' ? 'active' : ''
					}`"
				>
					<div
						class="book-list-names__name"
						@click="(event) => setSelectedList('wantToRead')"
					>
						Want to Read
					</div>
					<div class="book-list-count">({{ wantToReadCount }})</div>
				</div>
			</div>
		</div>
		<div class="main-container">
			<LoadingSpinner v-if="isLoading" />
			<MyBookList v-else :items="selectedList" />
		</div>
	</div>
</template>

<script>
	import { ref, computed, onMounted } from "vue";
	import { useStore } from "vuex";
	import LoadingSpinner from "@/components/Ui/Spinners/LoadingSpinner.vue";
	import MyBookList from "@/components/MyBooks/MyBookList.vue";

	export default {
		components: {
			LoadingSpinner,
			MyBookList,
		},
		setup: () => {
			const selectedName = ref("all");
			const { state, dispatch } = useStore();

			const isLoading = computed(() => state.bookLists.isLoading);

			const selectedList = computed(() => {
				switch (selectedName.value) {
					case "all":
						return state.bookLists.currentlyReading
							.concat(state.bookLists.wantToRead)
							.concat(state.bookLists.read);
					case "currentlyReading":
						return state.bookLists.currentlyReading;
					case "wantToRead":
						return state.bookLists.wantToRead;
					case "read":
						return state.bookLists.read;
					default:
						return [];
				}
			});

			const setSelectedList = (name) => {
				selectedName.value = name;
			};

			const currentlyReadingCount = computed(() => state.bookLists.currentlyReading.length);
			const readCount = computed(() => state.bookLists.read.length);
			const wantToReadCount = computed(() => state.bookLists.wantToRead.length);

			onMounted(() => {
				document.title = "BookSelf - My Books";
				dispatch("bookLists/fetchAll");
			});

			return {
				selectedList,
				selectedName,
				setSelectedList,
				currentlyReadingCount,
				readCount,
				wantToReadCount,
				isLoading,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./MyBooksPage";
</style>
