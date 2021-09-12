<template>
	<div class="reading-item-container">
		<div class="image-container">
			<img :src="image" :alt="title" />
		</div>
		<div class="details-container">
			<div>
				<!-- <router-link :to="`book/${id}`" class="book-link">
					<div class="title">{{ title }}</div>
				</router-link> -->
				<div class="title">
					<router-link :to="`/book/${id}`">
						{{ title }}
					</router-link>
				</div>
				<AuthorLinks :authors="authors" />
			</div>
			<div class="progress">
				<ProgressBar :total="totalPages" :done="pagesRead" />
				<span>{{ progressLabel }}</span>
			</div>
			<div class="button-container">
				<ButtonComponent :text="'Update progress'" />
			</div>
		</div>
	</div>
</template>

<script>
	import { computed } from "vue";
	import AuthorLinks from "./AuthorLinks.vue";
	import ProgressBar from "@/components/Ui/ProgressBar.vue";
	import ButtonComponent from "@/components/Ui/Buttons/ButtonComponent.vue";

	export default {
		components: {
			AuthorLinks,
			ProgressBar,
			ButtonComponent,
		},
		props: {
			id: Number,
			title: String,
			authors: Array,
			image: String,
			totalPages: Number,
			pagesRead: Number,
		},
		setup: (props) => {
			const progressLabel = computed(() => {
				const percentage = ((props.pagesRead / props.totalPages) * 100).toFixed(0);
				return `${props.pagesRead}/${props.totalPages} (${percentage}%)`;
			});
			const showAllAuthorsHandler = () => {
				console.log("Show others");
				//TODO: Implement
			};

			return {
				progressLabel,
				showAllAuthorsHandler,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./CurrentlyReadingItem";
</style>
