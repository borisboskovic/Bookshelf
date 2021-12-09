<template>
	<div class="book-list-item__container">
		<SimpleSpinner v-if="isSubmitting" :scale="3" />
		<div class="book-list-item__content">
			<router-link :to="`/book-details/${id}`">
				<div class="book-list-item__book-cover">
					<FallbackImage :defaultImage="defaultImage" :title="title" :source="image" />
				</div>
			</router-link>
			<div class="book-list-item__details-container">
				<div class="book-list-item__title">
					<router-link :to="`/book-details/${id}`">
						{{ title }}
					</router-link>
				</div>
				<AuthorLinks :authors="authors" className="book-list-item__authors" />
				<div v-if="ratings.average" class="book-list-item__ratings">
					<i class="fa fa-star"></i>
					<div>{{ ratings.average.toFixed(1) }}</div>
					<span>&middot;</span>
					<div class="rating-count__aligned-right">{{ ratings.count }} ratings</div>
				</div>
				<div v-else class="book-list-item__ratings">
					<i class="fa fa-star"></i>
					<span>Not rated</span>
				</div>
				<div class="book-list-item__my-rating">
					<span>My rating:</span>
					<div v-if="ratings.yourRating">
						<i class="fa fa-star"></i>
						<span>{{ ratings.yourRating }}</span>
					</div>
					<div v-else>Not rated</div>
				</div>
				<div class="book-list-item__date-added">
					<span>Date added:</span>
					<div>{{ formattedDate }}</div>
				</div>
			</div>
		</div>
		<div class="book-list-item__buttons">
			<div class="book-list-item__button-group">
				<ButtonGroup
					:list="list"
					@add-to-list="addToListHandler"
					@remove-from-list="removeFromListHandler"
				/>
			</div>
			<ButtonComponent size="small" :className="'book-list-item__btn'">
				Post a review
			</ButtonComponent>
		</div>
	</div>
</template>

<script>
	import { ref, computed } from "vue";
	import { useStore } from "vuex";
	import defaultImage from "@/assets/images/rasters/book-placeholder.jpg";
	import FallbackImage from "@/components/Ui/Imaging/FallbackImage.vue";
	import AuthorLinks from "@/components/Author/AuthorLinks.vue";
	import ButtonGroup from "@/components/Ui/Buttons/ButtonGroup.vue";
	import ButtonComponent from "@/components/Ui/Buttons/ButtonComponent.vue";
	import SimpleSpinner from "@/components/Ui/Spinners/SimpleSpinner.vue";

	export default {
		props: {
			id: Number,
			title: String,
			image: String,
			authors: Array,
			ratings: Object,
			addedOn: String,
			list: String,
		},
		components: {
			FallbackImage,
			AuthorLinks,
			ButtonGroup,
			ButtonComponent,
			SimpleSpinner,
		},
		setup: (props) => {
			const { dispatch } = useStore();

			const isSubmitting = ref(false);
			const formattedDate = computed(() => new Date(props.addedOn).toLocaleDateString("sr"));

			const addToListHandler = (listNames) => {
				isSubmitting.value = true;
				dispatch("bookLists/addToList", {
					id: props.id,
					...listNames,
				}).finally(() => {
					isSubmitting.value = false;
				});
			};
			const removeFromListHandler = (listName) => {
				isSubmitting.value = true;
				dispatch("bookLists/removeFromList", {
					id: props.id,
					list: listName,
				}).finally(() => {
					isSubmitting.value = false;
				});
			};

			return {
				defaultImage,
				formattedDate,
				isSubmitting,
				addToListHandler,
				removeFromListHandler,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./MyBookListItem";
</style>
